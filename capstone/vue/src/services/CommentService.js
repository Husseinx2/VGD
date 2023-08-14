import axios from 'axios';

export default {
    addComment(comment) {
        return axios.post(`/comment`, comment);
    },
    getUserComments(id) {
        return axios.get("comment/commenter/" + id);
    },
    getReviewComments(id) {
        return axios.get("comment/review/" + id);
    },
    getComment(id) {
        return axios.get("comment/" + id);
    },
    editComment(comment) {
        return axios.put('comment', comment);
    },
    deleteComment(id){
        return axios.delete( "comment/" + id);
    },
    deleteCommentByReviewId(id){
        return axios.delete( "comment/review/" + id);
    },
    deleteCommentByCommenterId(id){
        return axios.delete( "comment/commenter/" + id);
    }
};