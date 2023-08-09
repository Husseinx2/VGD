import axios from 'axios';

export default {
  getRating(user) {
    return axios.get(`/rating/${user.userRating}`, user)
    }
}
