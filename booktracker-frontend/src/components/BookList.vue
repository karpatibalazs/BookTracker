<script setup>
import { ref, onMounted } from 'vue'
import AddBookForm from './AddBookForm.vue'

const books = ref([])
const error = ref(null)

async function fetchBooks() {
  try {
    const response = await fetch('http://localhost:5146/api/books')
    if (!response.ok) throw new Error(`HTTP ${response.status}`)
    books.value = await response.json()
  } catch (err) {
    error.value = err.message
  }
}

async function deleteBook(id) {
  try {
    const response = await fetch(`http://localhost:5146/api/books/${id}`, {
      method: 'DELETE'
    })
    if (!response.ok) throw new Error(`HTTP ${response.status}`)
    await fetchBooks()
  } catch (err) {
    error.value = err.message
  }
}

function statusLabel(status) {
  const labels = { PlanToRead: 'Olvasnám', CurrentlyReading: 'Éppen olvasom', Completed: 'Elolvastam' }
  return labels[status] ?? status
}

function statusClass(status) {
  const classes = { PlanToRead: 'status-plan', CurrentlyReading: 'status-reading', Completed: 'status-done' }
  return classes[status] ?? ''
}

onMounted(fetchBooks)
</script>

<template>
  <div class="page">
    <header class="page-header">
      <h1>Könyveim</h1>
      <p class="subtitle">{{ books.length }} könyv a polcodon</p>
    </header>

    <AddBookForm @book-added="fetchBooks" />

    <p v-if="error" class="error">Hiba történt: {{ error }}</p>

    <ul v-else class="book-list">
      <li v-for="book in books" :key="book.bookId" class="book-row">
        <div class="book-info">
          <span class="book-name">{{ book.bookName }}</span>
          <span class="book-author">{{ book.bookAuthor }}</span>
        </div>
        <span class="status-pill" :class="statusClass(book.status)">{{ statusLabel(book.status) }}</span>
        <button class="delete-btn" @click="deleteBook(book.bookId)">Törlés</button>
      </li>
    </ul>
  </div>
</template>

<style scoped>
.page-header { margin-bottom: 1rem; }
.page-header h1 {
  font-family: var(--font-display);
  font-size: 2.25rem;
  margin: 0 0 0.25rem;
}
.subtitle { color: var(--color-text-muted); margin: 0; }

.error { color: #E08585; }

.book-list { list-style: none; margin: 2rem 0 0; padding: 0; }

.book-row {
  display: flex;
  align-items: center;
  gap: 1rem;
  padding: 0.9rem 0;
  border-bottom: 1px solid var(--color-border);
}

.book-info { flex: 1; display: flex; flex-direction: column; }
.book-name { font-family: var(--font-display); font-size: 1.05rem; }
.book-author { color: var(--color-text-muted); font-size: 0.9rem; }

.status-pill {
  font-size: 0.8rem;
  padding: 0.2rem 0.65rem;
  border-radius: 999px;
  border: 1px solid var(--color-border);
  color: var(--color-text-muted);
  white-space: nowrap;
}
.status-reading { border-color: var(--color-accent); color: var(--color-accent); }
.status-done { border-color: var(--color-done); color: var(--color-done); }

.delete-btn {
  background: none;
  border: none;
  color: var(--color-text-muted);
  cursor: pointer;
  font-size: 0.85rem;
  padding: 0.3rem 0.5rem;
}
.delete-btn:hover { color: #E08585; }
</style>