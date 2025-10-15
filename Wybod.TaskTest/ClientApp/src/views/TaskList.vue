<template>
  <div>
    <h2>Tasks</h2>
    <div>
      <button @click="setFilter('all')">All</button>
      <button @click="setFilter('completed')">Completed</button>
      <button @click="setFilter('pending')">Pending</button>
    </div>
    <div v-for="task in filteredTasks" :key="task.id">
      <router-link :to="`/task/${task.id}`">{{ task.title }}</router-link>
      <button @click="toggle(task)">✔</button>
      <button @click="remove(task.id)">🗑</button>
    </div>
  </div>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue';
import api from '../api';

const tasks = ref([]);
const filter = ref('all');

const fetch = async () => {
  const res = await api.get('/');
  tasks.value = res.data;
};

const toggle = async (task) => {
  task.isCompleted = !task.isCompleted;
  task.completedAt = task.isCompleted ? new Date().toISOString() : null;
  await api.put(`/${task.id}`, task);
  fetch();
};

const remove = async (id) => {
  await api.delete(`/${id}`);
  fetch();
};

const setFilter = (f) => filter.value = f;

const filteredTasks = computed(() =>
    filter.value === 'all' ? tasks.value :
        filter.value === 'completed' ? tasks.value.filter(t => t.isCompleted) :
            tasks.value.filter(t => !t.isCompleted)
);

onMounted(fetch);
</script>
