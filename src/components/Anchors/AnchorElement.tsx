import Anchor from '../../lib/Types/AnchorType'

type Callback = (id: string) => void; 
const AnchorElement = ({anchor, callback} : {anchor: Anchor, callback: Callback}) => 
{
  return (
    <>
        <span 
        className={anchor.AnchorId ? 'link' : ''}
        onClick={() => {
            // Jeżeli id nie jest puste
            if (anchor.AnchorId)
                callback(anchor.AnchorId)
            
        }}>{anchor.AnchorText}</span> 
    </>
  )
}

export default AnchorElement