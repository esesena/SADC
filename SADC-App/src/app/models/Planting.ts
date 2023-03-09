import { Farm } from './Farm';
import { Plot } from './Plot';
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
farm: Farm;
plot: Plot[];
}