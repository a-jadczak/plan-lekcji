import LessonCell from "./Cell/LessonCellType";

type TableRow = {
    nr: string,
    hour: string,
    lessonCells: LessonCell[]
}

export default TableRow;