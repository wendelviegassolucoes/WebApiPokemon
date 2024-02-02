const pokemonList = document.getElementById('pokemon-list');
const loadMoreButton = document.getElementById('load-more');
const loadAllButton = document.getElementById('load-all');
const loadingIndicator = document.getElementById('loading');
let currentPage = 1;
const pageSize = 100; // Tamanho da página

loadPokemons();
loadMoreButton.addEventListener('click', loadPokemons);
loadAllButton.addEventListener('click', loadAllPokemons);

function createDetailElement(key, value) {
    const detailItem = document.createElement('div');

    if (value === null || value === undefined) {
        // Se o valor for nulo ou indefinido, apenas retorne o detalhe vazio
        detailItem.textContent = `${key}: null`;
        return detailItem;
    }

    if (Array.isArray(value) && value.every(item => typeof item === 'object')) {
        // Se for uma matriz de objetos, exiba seus objetos em uma lista
        const list = document.createElement('ul');
        value.forEach(obj => {
            const listItem = document.createElement('li');
            const objDetails = document.createElement('ul');

            // Iterar sobre as propriedades do objeto dentro da matriz
            Object.entries(obj).forEach(([prop, val]) => {
                const objDetailItem = document.createElement('li');
                if (typeof val === 'object') {
                    // Se for um objeto, exiba suas propriedades e valores
                    if (Array.isArray(val)) {
                        // Se for uma matriz, chame recursivamente createDetailElement para lidar com cada item
                        objDetailItem.appendChild(createDetailElement(prop, val));
                    } else {
                        const subObjDetails = document.createElement('ul');
                        Object.entries(val).forEach(([subProp, subVal]) => {
                            const subObjDetailItem = document.createElement('li');
                            subObjDetailItem.textContent = `${subProp}: ${subVal}`;
                            subObjDetails.appendChild(subObjDetailItem);
                        });
                        objDetailItem.appendChild(subObjDetails);
                    }
                } else {
                    objDetailItem.textContent = `${prop}: ${val}`;
                }
                objDetails.appendChild(objDetailItem);
            });

            listItem.appendChild(objDetails);
            list.appendChild(listItem);
        });
        detailItem.textContent = `${key}:`;
        detailItem.appendChild(list);
    } else if (Array.isArray(value)) {
        // Se for uma matriz, exiba seus itens em uma lista
        const list = document.createElement('ul');
        value.forEach(item => {
            const listItem = document.createElement('li');
            listItem.textContent = item;
            list.appendChild(listItem);
        });
        detailItem.textContent = `${key}:`;
        detailItem.appendChild(list);
    } else if (typeof value === 'object') {
        // Se for um objeto, exiba suas propriedades e valores
        const objDetails = document.createElement('ul');
        Object.entries(value).forEach(([prop, val]) => {
            const objDetailItem = document.createElement('li');
            objDetailItem.textContent = `${prop}: ${val}`;
            objDetails.appendChild(objDetailItem);
        });
        detailItem.textContent = `${key}:`;
        detailItem.appendChild(objDetails);
    } else if (key === 'imageUrl') {
        // Se for uma URL de imagem, crie um elemento de imagem
        const img = document.createElement('img');
        img.src = value;
        img.alt = 'Pokemon Image';
        img.classList.add('pokemon-image');

        // Adiciona um ouvinte para o evento do clique do botão direito em qualquer imagem
        img.addEventListener("contextmenu", function (event) {
            event.preventDefault(); // Impede o menu padrão de aparecer

            // Verifica se o clique do botão direito foi em uma imagem
            if (event.target.tagName.toLowerCase() === 'img') {
                const imageURL = event.target.src; // Obtém a URL da imagem clicada

                // Cria um menu contextual
                const menu = document.createElement("div");
                menu.innerHTML = `
                        <ul>
                            <li id="feedPokemon">Alimentar Pokemon</li>
                        </ul>
                    `;
                menu.style.position = "absolute";
                menu.style.left = event.pageX + "px";
                menu.style.top = event.pageY + "px";
                menu.style.backgroundColor = "lightgray";
                menu.style.padding = "5px";
                menu.style.border = "1px solid gray";

                // Adiciona o menu ao corpo do documento
                document.body.appendChild(menu);

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
            }
        });

        img.addEventListener('click', function () {
            // Oculta a imagem do Pokémon
            img.style.visibility = 'hidden';

            // Exibe a Pokébola na mesma posição da imagem do Pokémon
            const pokeball = document.createElement('img');
            pokeball.classList.add('pokeball');
            pokeball.style.position = 'absolute';
            pokeball.style.left = `${img.offsetLeft}px`; // Define a posição da Pokébola com base na posição da imagem do Pokémon
            pokeball.style.top = `${img.offsetTop}px`; // Define a posição da Pokébola com base na posição da imagem do Pokémon
            pokemonList.appendChild(pokeball); // Adiciona a Pokébola ao mesmo contêiner que a imagem do Pokémon

            // Define um tempo para a Pokébola desaparecer e revelar a imagem do Pokémon novamente
            setTimeout(() => {
            }, 1000);
        });

        detailItem.appendChild(img);
    } else {
        // Para outros tipos, exiba o valor normalmente
        detailItem.textContent = `${value}`;
        detailItem.classList.add('pokemon-name');
    }

    return detailItem;
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