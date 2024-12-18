const nameGenerator = {
    init: function () {
        // Random Name Generator
        const generateNameBtn = document.getElementById('generateNameBtn');
        if (generateNameBtn) {
            generateNameBtn.addEventListener('click', () => this.generateName());
        }
        // Clan Name Generator
        const generateClanBtn = document.getElementById('generateClanBtn');
        if (generateClanBtn) {
            generateClanBtn.addEventListener('click', () => this.generateClanName());
        }
        // Scottish City Generator
        const generateCityBtn = document.getElementById('generateCityBtn');
        if (generateCityBtn) {
            generateCityBtn.addEventListener('click', () => this.generateCityName());
        }
    },
    generateName: async function () {
        try {
            const response = await fetch('Names/GetRandomName');
            if (!response.ok) throw new Error('Failed to generate name');
            const data = await response.json();
            document.getElementById('firstName').textContent = data.firstName;
            document.getElementById('lastName').textContent = data.lastName;
        } catch (error) {
            console.error('Error', error);
            alert('Failed to generate name. Please try again.');
        }
    },
    generateClanName: async function () {
        try {
            const response = await fetch('Names/GenerateClanName');
            if (!response.ok) {
                throw new Error('Failed to generate clan name');
            }
            const data = await response.json();
            document.getElementById('clanName').textContent = data.clanName;
        } catch (error) {
            console.error('Error:', error);
            alert('Failed to generate clan name. Please try again.');
        }
    },

    generateCityName: async function () {
        try {
            const response = await fetch('Names/GenerateCity');
            if (!response.ok) {
                throw new Error('Failed to generate city name');
            }
            const data = await response.json();
            /*alert(data.cityName);*/
            document.getElementById('albaCityName').textContent = data.cityName;
        } catch (error) {
            console.error('Error:', error);
            alert('Failed to generate Scottish city name. Please try again.');
        }
    }
};

document.addEventListener('DOMContentLoaded', () => {
    nameGenerator.init();
});