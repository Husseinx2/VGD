import axios from 'axios';

export default {
  getRating(user) {
    return axios.get(`/rating/${user.userRating}`, user)
    },
  getRatingByGameId(id){
    return axios.get(`/rating/game/${id}`);
  }
}
