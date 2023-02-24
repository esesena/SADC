import { Farm } from './Farm';

export interface Plot {
    Id: number;
    Name: string;
    Location: string;
    Size: number;
    SoilType: string;
    FarmId: number;
    Farm: Farm;
}
