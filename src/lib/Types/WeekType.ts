import LessonsColumn from "./Column/LessonsColumType";
import ScheduleColumn from "./Column/ScheduleColumnType";

type Week = {
    LessonCount: number,
    ScheduleColumns : ScheduleColumn[],
    LessonColumns: LessonsColumn[]
}

export default Week;