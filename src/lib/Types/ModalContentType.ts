import Entity from "./EntityType";

type Callback = (id: string) => void; 

type ModalContent = {
    Title: string,
    CategoryChar: string,
    Open: boolean,
    SetOpen: Function,
    Entities: Entity[] | undefined,
    Callback: Callback
}

export default ModalContent;