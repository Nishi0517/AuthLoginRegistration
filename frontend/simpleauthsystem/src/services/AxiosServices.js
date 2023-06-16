const axios = require('axios').default

export default class AxiosServices {
  post(url, data) {
    return axios.post('https://localhost:7298/api/Auth/SignUp', data)
  }
}
