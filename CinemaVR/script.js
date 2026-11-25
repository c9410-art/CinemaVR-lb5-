const screens = document.querySelectorAll('[data-screen]')
const screenLinks = document.querySelectorAll('[data-screen-link]')
const loginModal = document.querySelector('#login-modal')
const openLoginButtons = document.querySelectorAll('[data-open-login]')
const closeLoginButton = document.querySelector('[data-close-login]')
const seatGrid = document.querySelector('#seat-grid')
const selectedSeatsText = document.querySelector('#selected-seats')
const selectedSeats = new Set()
function showScreen(name) {
  screens.forEach((screen) => {
    screen.classList.toggle('active', screen.dataset.screen === name)
  })
  window.scrollTo({ top: 0, behavior: 'instant' })
}
screenLinks.forEach((link) => {
  link.addEventListener('click', () => showScreen(link.dataset.screenLink))
})
function openLogin() {
  loginModal.hidden = false
  document.body.style.overflow = 'hidden'
  loginModal.querySelector('input').focus()
}
function closeLogin() {
  loginModal.hidden = true
  document.body.style.overflow = ''
}
openLoginButtons.forEach((button) => {
  button.addEventListener('click', openLogin)
})
closeLoginButton.addEventListener('click', closeLogin)
loginModal.addEventListener('click', (event) => {
  if (event.target === loginModal) closeLogin()
})
document.addEventListener('keydown', (event) => {
  if (event.key === 'Escape' && !loginModal.hidden) closeLogin()
})
for (const row of ['A', 'B', 'C']) {
  for (let number = 1; number <= 16; number += 1) {
    const seatName = `${row}${number}`
    const button = document.createElement('button')
    button.className = 'seat'
    button.type = 'button'
    button.setAttribute('aria-label', `Місце ${seatName}`)
    button.setAttribute('aria-pressed', 'false')
    button.addEventListener('click', () => {
      if (selectedSeats.has(seatName)) {
        selectedSeats.delete(seatName)
      } else {
        selectedSeats.add(seatName)
      }
      const selected = selectedSeats.has(seatName)
      button.classList.toggle('selected', selected)
      button.setAttribute('aria-pressed', String(selected))
      selectedSeatsText.textContent = [...selectedSeats].join(', ')
    })
    seatGrid.append(button)
  }
}