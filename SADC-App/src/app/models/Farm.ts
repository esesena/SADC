import { Field } from './Field';
import { Employee } from './Employee';
import { User } from '../models/identity/User';

export interface Farm {
    id: number; 
    name: string; 
    location: string; 
    imageURL: string; 
    size: number; 
    fields: Field[]; 
}
