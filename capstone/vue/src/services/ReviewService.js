import axios from 'axios';

export default {
    addReview(review) {
        return axios.post(`/review`, review);
    },
    getUserReviews(id) {
        return axios.get(`review/reviewer/${id}`);
    },
    getGameReviews(id) {
        return axios.get(`review/game/${id}`);
    },
    getReview(id) {
        return axios.get(`review/${id}`);
    },
    editReview(review) {
        return axios.put('review', review);
    },
    deleteReview(id){
        return axios.put( `review/${id}`);
    },
    deleteReviewByGameId(id){
        return axios.put( `review/game/${id}`);
    },
    deleteReviewByReviewerId(id){
        return axios.put( `review/reviewer/${id}`);
    }

};
