import { forwardRef, Injectable } from "@angular/core";
import { User } from "../../domain/user/user.entity";
import { InventoryModule } from "../../inventory.module";
import { UserDTO } from "../equipment/DTOs/user.dto";

@Injectable({
  providedIn: forwardRef(() => InventoryModule)
})
export class UserMapper {
  mapToDTO(user: User): UserDTO {
    return {
      id: user.id,
      email: user.email,
      firstName: user.firstName,
      lastName: user.lastName
    };
  }

  mapToDomain(userDTO: UserDTO): User {
    return {
      id: userDTO.id,
      email: userDTO.email,
      firstName: userDTO.firstName,
      lastName: userDTO.lastName
    };
  }
}
