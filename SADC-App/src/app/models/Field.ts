import { Farm } from './Farm';

export interface Field {
    id: number;
    name: string;
    location: string;
    size: number;
    soilType: string;
    farmId: number;
    farm: Farm;
}
