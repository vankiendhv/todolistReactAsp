export const toggleMenu = (category, tag) => {
    let liCategoryAll = category.querySelectorAll("li");
    let liTagAll = tag.querySelectorAll("li");

    liCategoryAll.forEach((item) => {
        item.onclick = function () {
            let liActive = category.querySelector(".active");
            liActive.classList.remove("active");
            item.classList.add("active");
        };
    });
    liTagAll.forEach((item) => {
        item.onclick = function () {
            let liActive = tag.querySelector(".active");
            liActive.classList.remove("active");
            item.classList.add("active");
        };
    });
};
