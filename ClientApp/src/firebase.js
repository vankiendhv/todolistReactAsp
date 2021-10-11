import firebase from "firebase";
import "firebase/storage";
const config = {
    apiKey: "AIzaSyDEMuooIcCQ-_EG9-vmhW6wZ8z1OORy4_E",
    authDomain: "todolistreact-5707a.firebaseapp.com",
    projectId: "todolistreact-5707a",
    storageBucket: "todolistreact-5707a.appspot.com",
    messagingSenderId: "277027621231",
    appId: "1:277027621231:web:e325d0e8eecc0b5645c09a",
    measurementId: "G-YQY9MC4FHP",
};

firebase.initializeApp(config);

const storage = firebase.storage();
export { storage, firebase as default };
