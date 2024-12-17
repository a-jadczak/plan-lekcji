import React from 'react'
import AnchorElement from './AnchorElement'
import AnchorsRow from '../../lib/Types/AnchorsRowType'

type Callback = (id: string) => void;
const AnchorsRowElement = ({anchorsRow, callback} : {anchorsRow : AnchorsRow, callback: Callback}) => 
{
  return (
    <div className='anchors-row'>
      {anchorsRow.Anchors
        .sort((a, b) => a.AnchorType - b.AnchorType) // Sortowanie według AnchorType
        .map(anchor => 
          <AnchorElement anchor={anchor} callback={callback} />
        )}
    </div>
  );
}

export default AnchorsRowElement