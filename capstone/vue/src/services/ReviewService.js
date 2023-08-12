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
        axios.put('review', review);
    },
    deleteReview(id){
        axios.put( `review/${id}`);
    },
    deleteReviewByGameId(id){
        axios.put( `review/game/${id}`);
    },
    deleteReviewByReviewerId(id){
        axios.put( `review/reviewer/${id}`);
    }

};
