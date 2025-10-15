<template>
  <div class="detail-container" v-if="task">
    <h2>Task Details</h2>

    <div class="detail-card">
      <h3>{{ task.title }}</h3>
      <p>{{ task.description }}</p>

      <div class="status">
        <strong>Status:</strong>
        <span :class="task.isCompleted ? 'completed' : 'pending'">
          {{ task.isCompleted ? '✅ Completed' : '🕒 Pending' }}
        </span>
      </div>

      <div class="timestamps">
        <p><strong>Created:</strong> {{ formatDate(task.createdAt) }}</p>
        <p v-if="task.completedAt"><strong>Completed:</strong> {{ formatDate(task.completedAt) }}</p>
      </div>

      <div class="actions">
        <button @click="toggle">Toggle Status</button>
        <button @click="remove">Delete Task</button>
        <router-link to="/" class="back-link">← Back to List</router-link>
      </div>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { ref, onMounted } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import api from '../api';

interface TaskItem {
  id: string;
  title: string;
  description: string;
  isCompleted: boolean;
  createdAt: string;
  completedAt: string | null;
}

const route = useRoute();
const router = useRouter();
const task = ref<TaskItem | null>(null);

const fetchTask = async () => {
  const res = await api.get(`/${route.params.id}`);
  task.value = res.data;
};

const toggle = async () => {
  if (!task.value) return;
  const updated = {
    ...task.value,
    isCompleted: !task.value.isCompleted,
    completedAt: !task.value.isCompleted ? new Date().toISOString() : null
  };
  await api.put(`/${task.value.id}`, updated);
  await fetchTask();
};

const remove = async () => {
  if (!task.value) return;
  await api.delete(`/${task.value.id}`);
  router.push('/');
};

const formatDate = (date: string) => new Date(date).toLocaleString();

onMounted(fetchTask);
</script>

<style scoped>
.detail-container {
  max-width: 600px;
  margin: 2rem auto;
  font-family: 'Segoe UI', sans-serif;
}

h2 {
  text-align: center;
  margin-bottom: 1rem;
}

.detail-card {
  padding: 1.5rem;
  border: 1px solid #ccc;
  border-radius: 8px;
  background-color: #fefefe;
}

h3 {
  margin-bottom: 0.5rem;
}

.status {
  margin: 1rem 0;
  font-size: 1.1rem;
}

.status .completed {
  color: green;
}

.status .pending {
  color: orange;
}

.timestamps p {
  margin: 0.25rem 0;
  font-size: 0.95rem;
  color: #555;
}

.actions {
  margin-top: 1.5rem;
  display: flex;
  gap: 0.5rem;
  flex-wrap: wrap;
}

button {
  padding: 0.5rem 1rem;
  border: none;
  background-color: #0078d4;
  color: white;
  border-radius: 4px;
  cursor: pointer;
}

button:hover {
  background-color: #005fa3;
}

.back-link {
  margin-top: 0.5rem;
  display: inline-block;
  text-decoration: none;
  color: #0078d4;
}
</style>
