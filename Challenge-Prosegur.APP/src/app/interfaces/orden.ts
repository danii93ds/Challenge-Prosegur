import { Guid } from 'guid-typescript';
export interface Orden {
  id: Guid
  numero: number,
  usuariosid: Guid,
  nombre: string,
  productos: object
}
