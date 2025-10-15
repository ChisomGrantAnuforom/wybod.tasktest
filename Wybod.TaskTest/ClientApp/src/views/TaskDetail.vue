<template>
  <div v-if="task">
    <h2>{{ task.title }}</h2>
    <p>{{ task.description }}</p>
    <p>Status: {{ task.isCompleted ? 'Completed' : 'Pending' }}</p>
    <p v-if="task.completedAt">Completed At: {{ new Date(task.completedAt).toLocaleString() }}</p>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import { useRoute } from 'vue-router';
import api from '../api';

const route = useRoute();
const task = ref(null);

onMounted(async () => {
  const res = await api.get(`/${route.params.id}`);
  task.value = res.data;
});
</script>
