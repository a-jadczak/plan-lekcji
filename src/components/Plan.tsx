import { useState, useEffect } from 'react';
import LessonPlan from './../lib/Types/LessonPlanType';
import Entity from '../lib/Types/EntityType';
import TableRow from '../lib/Types/TableRowType';
import LessonCell from '../lib/Types/Cell/LessonCellType';
import Table from './Table/Table';
import TableMobile from './Table/TableMobile';
import IconPack from '../lib/Types/IconPackType';
import Print from './Print';
import iconDictionary from './../lib/Lists/IconDictionary';


const responsiveBreakPoint = 900;

const Plan = ({ plan, sendCallback } : { plan : LessonPlan | null, sendCallback : Function}) => {
    
    const [isSmallScreen, setIsSmallScreen] = useState<boolean>(window.innerWidth < responsiveBreakPoint);
    const [currentIconsPack, setCurrentIconsPack] = useState<IconPack>();

    const [selectedEntity, setSelectedEntity] = useState<Entity | null>();
    const [dataArray, setDataArray] = useState<TableRow[] | null>(null);

    const dayArray: string[] = ["Poniedziałek", "Wtorek", "Środa", "Czwartek", "Piątek"];

    const setEntityById = (id: string) =>
    {
        const foundEntity = plan?.Entities.find(entity => entity.Id == id);
        setSelectedEntity(foundEntity);
        fillDataArray(foundEntity);
        setCurrentIcons(foundEntity);
    }

    const fillDataArray = (entity : Entity | undefined) =>
    {        
        if (entity == undefined)
        {
            setDataArray(null);
            return;
        }

        let dataArr: TableRow[] = []

        // Każda kolumna ma taką samą długość więc biorę pierwszą dla uproszczenia
        const length = entity.Week.ScheduleColumns[0].Cells.length;

        for (let i = 0; i < length; i++) 
        {
            const nr: string = entity.Week.ScheduleColumns[0].Cells[i].Text;
            const hour: string = entity.Week.ScheduleColumns[1].Cells[i].Text;

            let lessonCells: LessonCell[] = [];

            // Każda kolumna lekcji
            entity.Week.LessonColumns.forEach(lessonCol => 
            {
                // Wybiera wszystkie kratki z wiersza o i index'ie
                lessonCells = [...lessonCells, lessonCol.Lessons[i]] 
            });

            dataArr = [...dataArr, {nr, hour, lessonCells}]
        }

        setDataArray(dataArr);
    }
    
    // W zależności od wybranego entity ustala obecne ikonki dla planu lekcji
    const setCurrentIcons = (entity : Entity | undefined) =>
    {
        if (entity == undefined)
        {
            return;
        }
                
        const character = entity.Id.charAt(0);

        setCurrentIconsPack(iconDictionary.get(character));
    }

    useEffect(() => sendCallback(setEntityById), [plan])
    
    // Obsługa responsywności planu lekcji
    useEffect(() => 
    {
        const handleResize = () => 
        {
          setIsSmallScreen(window.innerWidth < responsiveBreakPoint);
        };
    
        window.addEventListener('resize', handleResize);
    
        return () => 
        {
          window.removeEventListener('resize', handleResize);
        };
    }, []);
    

    return (
        <>
        {
            selectedEntity 
            ?
            <>
                {
                    isSmallScreen 
                    ? <TableMobile iconPack={currentIconsPack} selectedEntity={selectedEntity} dataArray={dataArray} dayArray={dayArray} callback={setEntityById}/>
                    : <Table iconPack={currentIconsPack} selectedEntity={selectedEntity} dataArray={dataArray} dayArray={dayArray} callback={setEntityById}/>
                }
                <Print/>
                
            </>
            : <h2 className='m-2'>Wybierz oddział</h2>
        }    
        <div className='container-fluid d-flex justify-content-center'>
            <div>
                <h5>Data aktualizacji planu: {plan?.LastModified.split("-").join(".")}</h5>
            </div>
        </div>

        </>
    )
}

export default Plan