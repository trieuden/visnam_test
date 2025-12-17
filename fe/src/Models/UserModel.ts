import type { RoleModel } from './RoleModel';

export type UserModel = {
  id: string;
  username: string;
  email: string;
  role: RoleModel;
};
