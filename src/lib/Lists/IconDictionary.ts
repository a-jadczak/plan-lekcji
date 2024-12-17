import IconPack from '../Types/IconPackType'
import ClassIcon from './../../../public/icons/class.svg'
import ClassroomIcon from './../../../public/icons/classroom.svg'
import TeacherIcon from './../../../public/icons/teacher.svg'

const iconDictionary = new Map<string, IconPack>([
    ["n", { firstCol: ClassIcon, secondCol: ClassroomIcon, selected: TeacherIcon }],
    ["o", { firstCol: TeacherIcon, secondCol: ClassroomIcon, selected: ClassIcon  }],
    ["s", { firstCol: ClassIcon, secondCol: TeacherIcon, selected: ClassroomIcon  }]
])

export default iconDictionary;