import Entity from '../../lib/Types/EntityType'
import IconPack from '../../lib/Types/IconPackType'
import TableRow from '../../lib/Types/TableRowType'
import AnchorsRowElement from '../Anchors/AnchorsRow'

const Table = ({iconPack, selectedEntity, dataArray, dayArray, callback}:{selectedEntity: Entity, 
    dataArray:TableRow[] | null, dayArray: string[], callback : Function, iconPack: IconPack | undefined}) => 
{

    return (
        <>
            <h2 className="m-2">
                <img className='icon' src={iconPack?.selected} />
                <span className="ms-3">
                    {selectedEntity.Title}
                </span>
            </h2>

            <table className="table table-striped">
                <thead>
                    <tr>
                        <th>Nr</th>
                        <th>Godzina</th>
                        {
                            // render table header
                            dayArray.map((e) =>
                            {
                                return (
                                <th key={e}>
                                    <div className='table-header-container'>
                                        <span>{e}</span>
                                        <img src={iconPack?.firstCol} className='icon' /> 
                                        <img src={iconPack?.secondCol} className='icon' /> 
                                    </div>
                                </th>)
                            })
                        }
                    </tr>
                </thead>
                <tbody>
                    {dataArray?.map(e => 
                        (<tr>
                            <td>{e.nr}</td>
                            <td>{e.hour}</td>

                            {e.lessonCells.map(lessonCell => 
                                lessonCell.ScheduleGap 
                                ? <td></td>
                                : 
                                <td>
                                    <div className='anchors-container'>
                                    {
                                        lessonCell.AnchorsRows.map(a => 
                                            //@ts-ignore
                                            <AnchorsRowElement anchorsRow={a} callback={callback}/>
                                        )
                                    }
                                    </div>
                                </td>
                            )}
                        </tr>)
                    )}
                </tbody>
            </table>
        </>
    )
}

export default Table