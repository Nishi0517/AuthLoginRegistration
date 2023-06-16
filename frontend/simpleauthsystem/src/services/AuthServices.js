import AxiosServices from './AxiosServices'
// import Configurations from '../configurations/Configurations'
import Configurations from '../configurations/Configuration'

const axiosServices = new AxiosServices();

export default class AuthServices {
  SignUp(data) {
    return axiosServices.post('https://localhost:7298/api/Auth/SignUp', data)
  }

  SignIn(data) {
    return axiosServices.post(Configurations.SignIn, data)
  }
}
