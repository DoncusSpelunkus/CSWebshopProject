import {Injectable} from "@angular/core";
import {User} from "../Entities/User";
import {UserService} from "../services/user.service";

@Injectable({ providedIn: 'root' })

export class UserState { // State class for data manipulation

  constructor(private userService: UserService) {
  }

  async postUser(user: User){
    let dto = {
      id: user.id,
      name: user.fullName,
      email: user.email,
      address: user.address,
      city: user.city,
      zip: user.zip,
      phone: user.phone,
    }
    await this.userService.postUser(dto)
  }


  async putUser(user: User){
    let dto = {
      id: user.id,
      name: user.fullName,
      email: user.email,
      address: user.address,
      city: user.city,
      zip: user.zip,
      phone: user.phone,
    }
    return await this.userService.putUser(dto);
  }

  async deleteUserById(id: number){
    return this.userService.deleteUserById(id);
  }

}
