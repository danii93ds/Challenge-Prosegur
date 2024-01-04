import { Guid } from 'guid-typescript';
export interface Usuario {
  id: Guid,
  Apellido: string,
  Nombre: string,
  DNI: string, 
  Rol: string
}
