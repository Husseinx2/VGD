import axios from 'axios';

export default {
  getRatingByGameId(id){
    return axios.get(`/rating/game/${id}`);
  },
  getRatingByUserId(game,id){
    return axios.get(`/rating/${game/id}`);
  },
  updateExistingRating(currentRating){
    return axios.put(`/rating/${currentRating.id}}, rating`);
  },
  addNewRating(newRating){
    return axios.post("/rating", newRating);
  },
  deleteCurrentRating(id){
    return axios.delete(`/rating/${id}`);
  }
}
