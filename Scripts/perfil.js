document.querySelectorAll('a[href^="#"]').forEach(anchor => {
    anchor.addEventListener('click', function (e) {
        e.preventDefault()

        document.querySelector(this.getAttribute('href')).scrollIntoView({
            behavior: 'smooth'
        })
    })
})

const switchOption = document.querySelectorAll('.switch__option')
switchOption.forEach(e => {
    e.addEventListener('click', () => {
        switchOption.forEach(elem => {
            elem.classList.remove('switch__active')
        })

        e.classList.add('switch__active')
    })
})
