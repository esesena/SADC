import { Farm } from './Farm';
import { Field } from './Field';
import { Seed } from './Seed';

export class Planting {
id: number;
plantingDate: Date;
harvest: string;
rainAmount: number;
typePlanting: string;
weatherPlanting: string;
airMoisture: number;
seedId: number;
seed: Seed;
seedAmount: number;
fertilizing: string;
farmId: number;
farm: Farm;
fieldId: number[];
fields: Field[];
}