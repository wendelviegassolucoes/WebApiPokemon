const pokemonList = document.getElementById('pokemon-list');
const loadMoreButton = document.getElementById('load-more');
const loadAllButton = document.getElementById('load-all');
const reviveButton = document.getElementById('revive');
const loadingIndicator = document.getElementById('loading');
let currentPage = 1;
const pageSize = 100;
let currentMenu = null;

loadAllPokemons();

// Event listeners
loadMoreButton.addEventListener('click', loadPokemons);
loadAllButton.addEventListener('click', loadAllPokemons);
reviveButton.addEventListener('click', revivePokemons);
document.addEventListener("click", closeMenu);

// Functions
function closeMenu(event) {
    if (currentMenu && !currentMenu.contains(event.target)) {
        currentMenu.remove();
        currentMenu = null;
    }
}

function createDetailElement(key, value) {
    const detailItem = document.createElement('div');

    if (value === null || value === undefined) {
        detailItem.textContent = `${key}: null`;
        return detailItem;
    }

    if (key === 'imageUrl') {
        const img = createImageElement(value);
        detailItem.appendChild(img);
    } else {
        detailItem.textContent = `${value}`;
        detailItem.classList.add(`pokemon-name-${value}`);
    }

    return detailItem;
}

function createImageElement(value) {
    const img = document.createElement('img');
    img.src = value;
    img.alt = 'Pokemon Image';
    img.classList.add(`pokemon-image-${value}`);

    img.addEventListener('click', function () {
        animatePokemon(img);
    });

    img.addEventListener("contextmenu", function (event) {
        event.preventDefault();
        showContextMenu(event, img.src);
    });

    return img;
}

function animatePokemon(img) {
    img.classList.remove('pokeball', 'open');
    img.classList.add('pokeball', 'closed');
    img.src = '../img/pokebola_fechada.png';
}

function showContextMenu(event, imageURL) {
    if (currentMenu) {
        currentMenu.remove();
    }

    const menu = document.createElement("div");
    menu.innerHTML = `
        <ul class="menu-items">
            <li id="feedPokemon"><button class="menu-btn"><i class="fa-solid fa-heart"></i>Alimentar Pokemon</button></li>
        </ul>
        <ul class="menu-items">
            <li id="killPokemon"><button class="menu-btn"><i class="fa-solid fa-ghost"></i></i>Matar Pokemon</button></li>
        </ul>
    `;

    menu.classList.add("menu");
    menu.style.left = event.pageX + "px";
    menu.style.top = event.pageY + "px";
    document.body.appendChild(menu);
    currentMenu = menu;

    document.getElementById("feedPokemon").addEventListener("click", feedPokemon.bind(null, imageURL, event));
    document.getElementById("killPokemon").addEventListener("click", killPokemon.bind(null, imageURL, event));

    document.addEventListener("click", closeMenu);
}

async function feedPokemon(imageURL, event) {
    try {
        const response = await fetch(`https://localhost:7081/Pokemon/FeedPokemon?pokemonId=${imageURL}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            }
        });
        if (response.ok) {
            showHeart(event);
        } else {
            console.error('Erro ao alimentar o Pokemon:', response.status);
        }
    } catch (error) {
        console.error('Erro ao alimentar o Pokemon:', error);
    }
}

async function killPokemon(value, event) {
    try {
        const response = await fetch(`https://localhost:7081/Pokemon/KillPokemon?pokemonId=${value}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            }
        });
        if (response.ok) {
            showBrokenHeart(event);
        } else {
            console.error('Erro ao matar o Pokemon:', response.status);
        }
    } catch (error) {
        console.error('Erro ao matar o Pokemon:', error);
    }
}

function showHeart(event) {
    const heart = createHeartElement();
    heart.style.position = "absolute";
    heart.style.left = (event.pageX + 50) + "px";
    heart.style.top = event.pageY + "px";
    document.body.appendChild(heart);
    setTimeout(() => {
        heart.remove();
    }, 3000);
}

function showBrokenHeart(event) {
    const heart = createBrokenHeartElement();
    heart.style.position = "absolute";
    heart.style.left = (event.pageX + 50) + "px";
    heart.style.top = event.pageY + "px";
    document.body.appendChild(heart);
    setTimeout(() => {
        heart.remove();
        location.reload();
    }, 3000);
}

function createHeartElement() {
    const heart = document.createElement("div");
    heart.id = "heart";
    heart.innerText = "❤️";
    return heart;
}

function createBrokenHeartElement() {
    const heart = document.createElement("div");
    heart.id = "heart";
    heart.innerText = "💔";
    return heart;
}

async function revivePokemons() {
    showLoadingIndicator();

    try {
        const response = await fetch(`https://localhost:7081/Pokemon/RevivePokemons`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            }
        });
        if (response.ok) {
            setTimeout(() => {
                location.reload();
            }, 1000);
        } else {
            console.error('Erro ao reviver os pokémons:', response.status);
        }
    } catch (error) {
        console.error('Erro ao reviver os pokémons:', error);
    } finally {
        hideLoadingIndicator();
    }
}

async function loadAllPokemons() {
    showLoadingIndicator();

    try {
        pokemonList.innerHTML = '';
        const response = await fetch('https://localhost:7081/Pokemon/GetAllPokemons');
        const data = await response.json();

        data.forEach(pokemon => {
            if (pokemon) {
                const pokemonDetails = document.createElement('div');
                Object.entries(pokemon).forEach(([key, value]) => {
                    const detailItem = createDetailElement(key, value);
                    pokemonDetails.appendChild(detailItem);
                });
                pokemonList.appendChild(pokemonDetails);
            }
        });
        setTimeout(() => {}, 1000);
    } catch (error) {
        console.error('Erro ao carregar todos os pokémons:', error);
    } finally {
        hideLoadingIndicator();
    }
}

async function loadPokemons() {
    showLoadingIndicator();

    try {
        const response = await fetch(`https://localhost:7081/Pokemon/GetPokemons?pageNumber=${currentPage}&pageSize=${pageSize}`);
        const data = await response.json();

        data.forEach(pokemon => {
            const pokemonDetails = document.createElement('div');
            Object.entries(pokemon).forEach(([key, value]) => {
                const detailItem = createDetailElement(key, value);
                pokemonDetails.appendChild(detailItem);
            });
            pokemonList.appendChild(pokemonDetails);
        });

        setTimeout(() => {}, 1000);
        currentPage++;
    } catch (error) {
        console.error('Erro ao carregar pokémons:', error);
    } finally {
        hideLoadingIndicator();
    }
}

function showLoadingIndicator() {
    loadingIndicator.style.display = 'block';
    loadingIndicator.classList.add('loading');
}

function hideLoadingIndicator() {
    loadingIndicator.style.display = 'none';
}
