async function loadAuthors() {
    // 1. Вземаме стойностите от полетата
    let search = document.getElementById("search").value;
    let specialty = document.getElementById("specialty").value;
    let sortOrder = document.getElementById("sortOrder").value;

    // 2. Подготвяме адреса на заявката
    let params = new URLSearchParams({
        search: search,
        specialty: specialty,
        sortOrder: sortOrder
    });

    // 3. Изпращаме заявката към контролера
    let response = await fetch(
        "/Author/GetFilteredAuthors?" + params
    );

    // 4. Преобразуваме JSON отговора в JavaScript обекти
    let author = await response.json();

    // 5. Изчистваме старите редове от таблицата
    let tbody = document.getElementById("authorTable");
    tbody.innerHTML = "";

    // 6. Добавяме ред за всеки намерен лекар
    author.forEach(function (author) {
        let row = document.createElement("tr");

        [author.firstName, author.lastName,
        author.country].forEach(function (value) {
            let cell = document.createElement("td");
            cell.textContent = value ?? "";
            row.appendChild(cell);
        });

        // Колона с линкове за действия
        let actions = document.createElement("td");

        ["Edit", "Details", "Delete"].forEach(function (action, index) {
            if (index > 0) {
                actions.appendChild(document.createTextNode(" | "));
            }

            let link = document.createElement("a");
            link.href = "/Author/" + action + "/" + author.id;
            link.textContent = action;
            actions.appendChild(link);
        });

        row.appendChild(actions);
        tbody.appendChild(row);
    });
}

// Автоматично филтриране
document.getElementById("search")
    .addEventListener("input", loadAuthors);

document.getElementById("specialty")
    .addEventListener("input", loadAuthors);

document.getElementById("sortOrder")
    .addEventListener("change", loadAuthors);

// Изчистване на филтрите
document.getElementById("clearFilters")
    .addEventListener("click", function () {
        document.getElementById("search").value = "";
        document.getElementById("specialty").value = "";
        document.getElementById("sortOrder").value = "name_asc";

        loadAuthors();
    });