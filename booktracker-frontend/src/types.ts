export type ReadingStatus = 'PlanToRead' | 'CurrentlyReading' | 'Completed'

export interface Book {
  bookId: number
  bookName: string
  bookAuthor: string
  bookRating: number
  status: ReadingStatus
}