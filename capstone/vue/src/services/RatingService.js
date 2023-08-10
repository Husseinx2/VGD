import axios from 'axios';

export default {
  listRatingsByGameId(gameId) {
    return axios.get(`/rating/game/${gameId}`);
  },
  listRatingsByUserId(userId) {
    return axios.get(`/rating/user/${userId}`);
  },
  getRating(gameId, userId) {
    return axios.get(`/rating/${gameId}/${userId}`)
  },
  addRating(rating) {
    return axios.post("/rating", rating);
  },
  updateRating(rating) {
    return axios.put('/rating', rating);
  },
  deleteRating(gameId, userId) {
    return axios.delete(`/rating/${gameId}/${userId}`);
  },
  deleteRatingsByGameId(gameId) {
    return axios.delete(`/rating/game/${gameId}`);
  },
  deleteRatingsByUserId(userId) {
    return axios.delete(`/rating/user/${userId}`);
  },
}
