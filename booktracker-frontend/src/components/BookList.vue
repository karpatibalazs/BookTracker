<script setup lang="ts">
import { ref, onMounted, computed } from 'vue'
import AddBookForm from './AddBookForm.vue'
import type { Book, ReadingStatus } from '../types'

const books = ref<Book[]>([])
const error = ref<string | null>(null)
const searchQuery = ref<string>("")
const filteredBooks = computed(() => books.value.filter(book => book.bookName.toLowerCase().includes(searchQuery.value.toLowerCase())))

async function fetchBooks() {
  try {
    const response = await fetch(`${import.meta.env.VITE_API_URL}/api/books`)
    if (!response.ok) throw new Error(`HTTP ${response.status}`)
    books.value = await response.json()
  } catch (err) {
    error.value = err instanceof Error ? err.message : String(err)
  }
}

async function deleteBook(id: number) {
  try {
    const response = await fetch(`${import.meta.env.VITE_API_URL}/api/books/${id}`, {
      method: 'DELETE'
    })
    if (!response.ok) throw new Error(`HTTP ${response.status}`)
    await fetchBooks()
  } catch (err) {
    error.value = err instanceof Error ? err.message : String(err)
  }
}

async function updateStatus(id : number, newStatus : ReadingStatus) {
  try {
    const response = await fetch(`${import.meta.env.VITE_API_URL}/api/books/${id}`, {
      method :'PUT',
      headers: { 'Content-Type' : 'application/json' },
      body : JSON.stringify({ status : newStatus })
    })
    if (!response.ok) throw new Error(`HTTP ${response.status}`)
    await fetchBooks()
  } catch (err) {
    error.value = err instanceof Error ? err.message : String(err)
  }
}


function statusClass(status: ReadingStatus): string {
  const classes: Record<ReadingStatus, string> = {
    PlanToRead: 'status-plan',
    CurrentlyReading: 'status-reading',
    Completed: 'status-done'
  }
  return classes[status]
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
    <input v-model="searchQuery" type="text" class="search-input" placeholder="Keresés cím szerint">
    <p v-if="error" class="error">Hiba történt: {{ error }}</p>
    
    <ul v-else class="book-list">
      <li v-for="book in filteredBooks" :key="book.bookId" class="book-row">
        <div class="book-info">
          <span class="book-name">{{ book.bookName }}</span>
          <span class="book-author">{{ book.bookAuthor }}</span>
        </div>
        <select
          class="status-pill"
          :class="statusClass(book.status)"
          :value="book.status"
          @change="updateStatus(book.bookId, ($event.target as HTMLSelectElement).value as ReadingStatus)"
        >
          <option value="PlanToRead">Tervezem olvasni</option>
          <option value="CurrentlyReading">Éppen olvasom</option>
          <option value="Completed">Elolvastam</option>
        </select>
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

.search-input {
  width: 100%;
  max-width: 320px;
  margin: 1.5rem 0 0.5rem;
  padding: 0.6rem 1rem;
  font-family: var(--font-body);
  font-size: 0.95rem;
  border: 1px solid var(--color-border);
  border-radius: 999px;
  background: var(--color-surface);
  color: var(--color-text);
  transition: border-color 0.15s ease;
}

.search-input::placeholder {
  color: var(--color-text-muted);
}

.search-input:focus {
  outline: none;
  border-color: var(--color-accent);
}

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
  font-family: var(--font-body);
  font-size: 0.8rem;
  padding: 0.3rem 0.75rem;
  border-radius: 999px;
  border: 1px solid var(--color-border);
  background: var(--color-surface);
  color: var(--color-text-muted);
  white-space: nowrap;
  cursor: pointer;
}
.status-pill:focus {
  outline: 2px solid var(--color-accent);
  outline-offset: 1px;
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