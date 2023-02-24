import { Plot } from './Plot';
import { Employee } from './Employee';
import { User } from '../models/identity/User';

export interface Farm {
    id: number; 
    name: string; 
    location: string; 
    imageURL: string; 
    size: number; 
    userId: number; 
    user: User; 
    plots: Plot[]; 
}
