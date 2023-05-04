import { forwardRef, Injectable } from "@angular/core";
import { User } from "../../domain/external/user.entity";
import { InventoryModule } from "../../inventory.module";
import { UserDTO } from "../equipment/DTOs/user.dto";

@Injectable({
  providedIn: forwardRef(() => InventoryModule)
})
export class UserMapper {
  mapToDTO(user: User): UserDTO {
    return {
      id: user.Id,
      email: user.Email,
      firstName: user.FirstName,
      lastName: user.LastName
    };
  }

  mapToDomain(userDTO: UserDTO): User {
    return {
      Id: userDTO.id,
      Email: userDTO.email,
      FirstName: userDTO.firstName,
      LastName: userDTO.lastName
    };
  }
}
