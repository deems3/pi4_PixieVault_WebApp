// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Function to create random stars
function createStars(numStars) {
    const hero = document.querySelector('.hero');

    for (let i = 0; i < numStars; i++) {
        const star = document.createElement('div');
        star.classList.add('star');

        // Random position
        const randomX = Math.random() * 100;
        const randomY = Math.random() * 100;

        // Random animation delay and duration
        const animationDelay = Math.random() * 5 + 's';
        const animationDuration = Math.random() * 5 + 5 + 's'; // Between 5s and 10s

        // Apply styles
        star.style.top = randomY + '%';
        star.style.left = randomX + '%';
        star.style.animationDelay = animationDelay;
        star.style.animationDuration = animationDuration;

        // Append star to hero
        hero.appendChild(star);
    }
}

// Generate 10 random stars
createStars(40);