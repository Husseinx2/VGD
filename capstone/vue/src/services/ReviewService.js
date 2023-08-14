import axios from 'axios';

export default {
    addReview(review) {
        return axios.post(`/review`, review);
    },
    getUserReviews(id) {
        return axios.get("review/reviewer/" + id);
    },
    getGameReviews(id) {
        return axios.get("review/game/" + id);
    },
    getReview(id) {
        return axios.get("review/" + id);
    },
    editReview(review) {
        return axios.put('review', review);
    },
    deleteReview(id){
        return axios.delete("review/" + id);
    },
    deleteReviewByGameId(id){
        return axios.delete("review/game/" + id);
    },
    deleteReviewByReviewerId(id){
        return axios.delete("review/reviewer/" + id);
    }
};