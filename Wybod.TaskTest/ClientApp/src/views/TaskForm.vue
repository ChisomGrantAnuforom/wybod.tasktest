<template>
  <div class="form-container">
    <h2>Create a New Task</h2>
    <form @submit.prevent="submit">
      <div class="form-group">
        <label for="title">Title</label>
        <input id="title" v-model="title" required />
      </div>

      <div class="form-group">
        <label for="description">Description</label>
        <textarea id="description" v-model="description" required></textarea>
      </div>

      <button type="submit">Create Task</button>
    </form>
  </div>
</template>

<script lang="ts" setup>
import { ref } from 'vue';
import api from '../api';
import { useRouter } from 'vue-router';

const title = ref('');
const description = ref('');
const router = useRouter();

const submit = async () => {
  await api.post('/', {
    title: title.value,
    description: description.value,
    isCompleted: false
  });
  router.push('/');
};
</script>

<style scoped>
.form-container {
  max-width: 500px;
  margin: 2rem auto;
  padding: 2rem;
  border: 1px solid #ddd;
  border-radius: 8px;
  background-color: #fefefe;
  font-family: 'Segoe UI', sans-serif;
}

h2 {
  text-align: center;
  margin-bottom: 1.5rem;
}

.form-group {
  margin-bottom: 1rem;
}

label {
  display: block;
  margin-bottom: 0.5rem;
  font-weight: 600;
}

input,
textarea {
  width: 100%;
  padding: 0.5rem;
  border: 1px solid #ccc;
  border-radius: 4px;
  font-size: 1rem;
}

textarea {
  resize: vertical;
  min-height: 100px;
}

button {
  display: block;
  width: 100%;
  padding: 0.75rem;
  background-color: #0078d4;
  color: white;
  border: none;
  border-radius: 4px;
  font-size: 1rem;
  cursor: pointer;
}

button:hover {
  background-color: #005fa3;
}
</style>



