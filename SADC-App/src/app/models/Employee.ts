import { Farm } from "./Farm";
import { UserUpdate } from "./identity/UserUpdate";

export interface Employee {
  id: number;
  firstName: string;
  lastName: string;
  cpf: string;
  function: string;
  workload: number;
  birthDate: Date;
  address: string;
  cep: string;
  city: string;
  state: string;
  imageURL: string;
  farm: Farm[];
}
