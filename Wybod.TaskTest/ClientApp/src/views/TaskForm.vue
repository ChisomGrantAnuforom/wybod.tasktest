<template>
  <div>
    <h2>Create Task</h2>
    <form @submit.prevent="submit">
      <div>
        <input v-model="title" placeholder="Title" required />
      </div>
      <div>
        <textarea v-model="description" placeholder="Description" required></textarea>
      </div>
      <button type="submit">Create</button>
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

