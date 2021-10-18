importScripts("https://www.gstatic.com/firebasejs/8.2.0/firebase-app.js");
importScripts("https://www.gstatic.com/firebasejs/8.2.0/firebase-messaging.js");

firebase.initializeApp({
    apiKey: "AIzaSyDEMuooIcCQ-_EG9-vmhW6wZ8z1OORy4_E",
    authDomain: "todolistreact-5707a.firebaseapp.com",
    projectId: "todolistreact-5707a",
    storageBucket: "todolistreact-5707a.appspot.com",
    messagingSenderId: "277027621231",
    appId: "1:277027621231:web:e325d0e8eecc0b5645c09a",
    measurementId: "G-YQY9MC4FHP",
});

const messaging = firebase.messaging();

messaging.onBackgroundMessage(function (payload) {
    const notificationTitle = payload.notification.title;
    const notificationOptions = {
        body: payload.notification.body,
    };
    self.registration.showNotification(notificationTitle, notificationOptions);
});
