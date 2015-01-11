using FMIRatingsAPI.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;

namespace FMIRatingsAPI.Statistics
{
    internal enum Instruction
    {
        MAP,
        FILTER,
        JOIN
    }

    internal class Transformation
    {
        private IQueryable _queryableData;
        private Type _resultType;
        private String _transform;
        private Dictionary<String, Object> _externals;

        public Transformation(String resType, String transform, FMIRatingsContext dbContext)
        {
            String tableName = transform.Split(new char[] { '.' }, 2)[0];
            _transform = transform;

            PropertyInfo propInfo = typeof(FMIRatingsContext).GetProperty(tableName);
            dynamic tableContents = propInfo.GetValue(dbContext);

            _queryableData = Queryable.AsQueryable(tableContents);

            Type resultBaseType = Type.GetType(resType);
            _resultType = typeof(IEnumerable<>).MakeGenericType(resultBaseType);

            Dictionary<String, Object> externals = new Dictionary<string, object>();
            externals.Add(tableName, tableContents);

            _externals = externals;
        }

        public Object Transform(Object input)
        {
            if (! _externals.ContainsKey("Input"))
            {
                _externals.Add("Input", input);
            }
            Expression expr = System.Linq.Dynamic.DynamicExpression.Parse(_resultType, _transform, _externals);
            return _queryableData.Provider.CreateQuery(expr);
        }
    }

    public class InputTransformer
    {
        private readonly FMIRatingsContext _dbContext;
        private Dictionary<String, Transformation> _argumentTransforms = new Dictionary<string,Transformation>();

        private String _transform;

        public InputTransformer(String transform, FMIRatingsContext dbContext)
        {
            _dbContext = dbContext;
            _transform = transform;

            ParseArgumentTransforms();
        }

        public Dictionary<String, Object> Transform(Object input)
        {
            Dictionary<String, Object> result = new Dictionary<string,object>(_argumentTransforms.Count);

            foreach (var argTransform in _argumentTransforms)
            {
                result.Add(argTransform.Key, argTransform.Value.Transform(input));
            }

            return result;
        }

        private void ParseArgumentTransforms()
        {
            String[] lines = _transform.Split('\n');
            foreach (String l in lines)
            {
                String[] splitLine = l.Split(new char[] { '=' }, 2);
                String[] splitArg = splitLine[0].Split(new char[] { ':' }, 2);
                String argumentName = splitArg[0].Trim();
                String argumentType = splitArg[1].Trim();
                String argumentTransformText = splitLine[1].Trim();

                Transformation t = new Transformation(argumentType, argumentTransformText, _dbContext);
                _argumentTransforms.Add(argumentName, t);
            }
        }
    }
}