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