run <- function(teacherNames, scores) {
library(rjson)

tNames <- fromJSON(URLdecode(teacherNames))
tScores <- fromJSON(scores)

p <- WebPlot(600, 600)

barplot(tScores, names.arg=tNames)

p
}