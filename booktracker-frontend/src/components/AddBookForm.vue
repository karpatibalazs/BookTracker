<script setup>
import { ref } from 'vue'

const bookName = ref('')
const bookAuthor = ref('')
const status = ref('PlanToRead')
const error = ref(null)

const emit = defineEmits(['book-added'])

async function submitForm() {
  error.value = null
  try {
    const response = await fetch(`${import.meta.env.VITE_API_URL}/api/books`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({
        bookName: bookName.value,
        bookAuthor: bookAuthor.value,
        status: status.value
      })
    })
    if (!response.ok) throw new Error(`HTTP ${response.status}`)

    bookName.value = ''
    bookAuthor.value = ''
    status.value = 'PlanToRead'

    emit('book-added')
  } catch (err) {
    error.value = err.message
  }
}
</script>

<template>
  <form @submit.prevent="submitForm">
    <h3>Új könyv</h3>
    <p v-if="error" class="error">Hiba történt: {{ error }}</p>

    <input v-model="bookName" placeholder="Cím" required />
    <input v-model="bookAuthor" placeholder="Szerző" required />
    <select v-model="status">
      <option value="PlanToRead">Tervezem olvasni</option>
      <option value="CurrentlyReading">Éppen olvasom</option>
      <option value="Completed">Elolvastam</option>
    </select>

    <button type="submit">Hozzáadás</button>
  </form>
</template>

<style scoped>
form {
  display: flex;
  flex-wrap: wrap;
  align-items: center;
  gap: 0.75rem;
  padding-bottom: 1.5rem;
  border-bottom: 1px solid var(--color-border);
}

h3 {
  width: 100%;
  font-family: var(--font-body);
  font-size: 0.85rem;
  font-weight: 500;
  color: var(--color-text-muted);
  margin: 0 0 0.25rem;
}

.error { width: 100%; color: #E08585; margin: 0; }

input, select {
  background: var(--color-surface);
  border: 1px solid var(--color-border);
  color: var(--color-text);
  padding: 0.55rem 0.75rem;
  border-radius: 6px;
  font-family: var(--font-body);
  font-size: 0.95rem;
}
input::placeholder { color: var(--color-text-muted); }

button[type='submit'] {
  background: var(--color-accent);
  color: #1B1F2A;
  border: none;
  padding: 0.55rem 1.1rem;
  border-radius: 6px;
  font-family: var(--font-body);
  font-weight: 600;
  cursor: pointer;
}
button[type='submit']:hover { opacity: 0.9; }
</style>