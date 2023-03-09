import { Farm } from "./Farm";
import { UserUpdate } from "./identity/UserUpdate";

export interface Employee {
  id: number;
  name: string;
  cpf: string;
  function: string;
  workload: number;
  birthDate: Date;
  address: string;
  cep: string;
  city: string;
  state: string;
  imageURL: string;
  userId: number;
  user: UserUpdate;
  farm: Farm[];
}
