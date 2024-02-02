const pokemonList = document.getElementById('pokemon-list');
const loadMoreButton = document.getElementById('load-more');
const loadAllButton = document.getElementById('load-all');
const reviveButton = document.getElementById('revive');
const loadingIndicator = document.getElementById('loading');
let currentPage = 1;
const pageSize = 100; // Tamanho da página
let currentMenu = null; // Variável para armazenar o menu atualmente aberto

loadPokemons();
loadMoreButton.addEventListener('click', loadPokemons);
loadAllButton.addEventListener('click', loadAllPokemons);
reviveButton.addEventListener('click', revivePokemons);

document.addEventListener("click", function (event) {
    if (currentMenu && !currentMenu.contains(event.target)) {
        currentMenu.remove();
        currentMenu = null;
    }
});


function createDetailElement(key, value) {
    const detailItem = document.createElement('div');

    if (value === null || value === undefined) {
        // Se o valor for nulo ou indefinido, apenas retorne o detalhe vazio
        detailItem.textContent = `${key}: null`;
        return detailItem;
    }

    if (key === 'imageUrl') {
        // Se for uma URL de imagem, crie um elemento de imagem
        const img = document.createElement('img');
        img.src = value;
        img.alt = 'Pokemon Image';
        img.classList.add(`pokemon-image-${value}`);

        img.addEventListener('click', function () {
            img.classList.remove(`pokemon-name-${value}`);
            img.classList.add('pokeball', 'open');
            img.src = '../img/pokebola_aberta.png'; // Define o src da Pokébola aberta
            img.style.border = 'none';
            img.style.left = `${img.offsetLeft}px`;
            img.style.top = `${img.offsetTop}px`;

            setTimeout(() => {
                img.classList.add('closed');
                img.src = '../img/pokebola_fechada.png'; // Define o src da Pokébola fechada
            }, 1000);
        });


        // Adiciona um ouvinte para o evento do clique do botão direito em qualquer imagem
        img.addEventListener("contextmenu", function (event) {
            event.preventDefault(); // Impede o menu padrão de aparecer

            // Verifica se o clique do botão direito foi em uma imagem
            if (event.target.tagName.toLowerCase() === 'img') {
                const imageURL = event.target.src; // Obtém a URL da imagem clicada

                // Fecha o menu atualmente aberto, se houver
                if (currentMenu) {
                    currentMenu.remove();
                }

                // Cria um menu contextual
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

                // Adiciona um ouvinte para o clique na opção "Alimentar Pokemon"
                document.getElementById("feedPokemon").addEventListener("click", function () {
                    // Simula uma chamada para um endpoint (substitua pelo seu endpoint real)
                    fetch(`https://localhost:7081/Pokemon/FeedPokemon?pokemonId=${img.src}`, {
                        method: 'PUT',
                        headers: {
                            'Content-Type': 'application/json'
                        }
                    })
                        .then(response => {
                            if (response.ok) {
                                // Exibe o coração após o sucesso da chamada do endpoint
                                const heart = document.createElement("div");
                                heart.id = "heart";
                                heart.innerText = "❤️";
                                heart.style.position = "absolute";
                                heart.style.left = (event.pageX + 50) + "px"; // Posiciona o coração ao lado do Pokémon
                                heart.style.top = event.pageY + "px";
                                document.body.appendChild(heart);

                                setTimeout(() => {
                                    // Remove o coração após 1 segundo
                                    heart.remove();
                                }, 3000);

                            } else {
                                console.error('Erro ao alimentar o Pokemon:', response.status);
                            }
                        })
                        .catch(error => {
                            console.error('Erro ao alimentar o Pokemon:', error);
                        });

                    // Remove o menu após selecionar a opção
                    menu.remove();
                });

                // Adiciona um ouvinte para clicar fora do menu para removê-lo
                document.addEventListener("click", function (e) {
                    if (!menu.contains(e.target)) {
                        menu.remove();
                    }
                });

                // Adiciona um ouvinte para o clique na opção "Alimentar Pokemon"
                document.getElementById("killPokemon").addEventListener("click", function () {
                    // Simula uma chamada para um endpoint (substitua pelo seu endpoint real)
                    fetch(`https://localhost:7081/Pokemon/KillPokemon?pokemonId=${value}`, {
                        method: 'PUT',
                        headers: {
                            'Content-Type': 'application/json'
                        }
                    })
                        .then(response => {
                            if (response.ok) {
                                // Exibe o coração após o sucesso da chamada do endpoint
                                const heart = document.createElement("div");
                                heart.id = "heart";
                                heart.innerText = "💔";
                                heart.style.position = "absolute";
                                heart.style.left = (event.pageX + 50) + "px"; // Posiciona o coração ao lado do Pokémon
                                heart.style.top = event.pageY + "px";
                                document.body.appendChild(heart);

                                setTimeout(() => {
                                    // Remove o coração após 1 segundo
                                    heart.remove();
                                    location.reload();
                                }, 3000);


                            } else {
                                console.error('Erro ao alimentar o Pokemon:', response.status);
                            }
                        })
                        .catch(error => {
                            console.error('Erro ao alimentar o Pokemon:', error);
                        });

                    // Remove o menu após selecionar a opção
                    menu.remove();
                });
            }
        });

        detailItem.appendChild(img);
    } else {
        // Para outros tipos, exiba o valor normalmente
        detailItem.textContent = `${value}`;
        detailItem.classList.add(`pokemon-name-${value}`);
    }

    return detailItem;
}

function revivePokemons() {
    // Mostra o indicador de carregamento
    loadingIndicator.style.display = 'block';
    loadingIndicator.classList.add('loading');

    try {
        fetch(`https://localhost:7081/Pokemon/RevivePokemons`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            }
        })

        setTimeout(() => {
            location.reload();
        }, 1000);

    } catch (error) {
        console.error('Erro ao carregar todos os pokémons:', error);
    } finally {
        // Esconde o indicador de carregamento após o carregamento
        loadingIndicator.style.display = 'none';
    }
}

async function loadAllPokemons() {
    // Mostra o indicador de carregamento
    loadingIndicator.style.display = 'block';
    loadingIndicator.classList.add('loading');



    try {
        pokemonList.innerHTML = '';

        const response = await fetch('https://localhost:7081/Pokemon/GetAllPokemons');
        const data = await response.json();

        data.forEach(pokemon => {
            if (pokemon) { // Verifica se pokemon não é null ou undefined
                const pokemonDetails = document.createElement('div');

                // Iterar sobre as propriedades do objeto pokemon
                for (const [key, value] of Object.entries(pokemon)) {
                    const detailItem = createDetailElement(key, value);
                    pokemonDetails.appendChild(detailItem);
                }

                pokemonList.appendChild(pokemonDetails);
            }
        });

        setTimeout(() => {
        }, 1000);

    } catch (error) {
        console.error('Erro ao carregar todos os pokémons:', error);
    } finally {
        // Esconde o indicador de carregamento após o carregamento
        loadingIndicator.style.display = 'none';
    }
}

async function loadPokemons() {
    // Mostra o indicador de carregamento
    loadingIndicator.style.display = 'block';
    loadingIndicator.classList.add('loading');

    try {
        const response = await fetch(`https://localhost:7081/Pokemon/GetPokemons?pageNumber=${currentPage}&pageSize=${pageSize}`);
        const data = await response.json();

        data.forEach(pokemon => {
            const pokemonDetails = document.createElement('div');

            // Iterar sobre as propriedades do objeto pokemon
            for (const [key, value] of Object.entries(pokemon)) {
                const detailItem = createDetailElement(key, value);
                pokemonDetails.appendChild(detailItem);
            }

            pokemonList.appendChild(pokemonDetails);
        });

        setTimeout(() => {
        }, 1000);

        currentPage++;
    } catch (error) {
        console.error('Erro ao carregar pokémons:', error);
    } finally {
        // Esconde o indicador de carregamento após o carregamento
        loadingIndicator.style.display = 'none';
    }
}