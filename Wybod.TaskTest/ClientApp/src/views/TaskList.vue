<template>
  <div>
    <h2>Tasks</h2>

    <div style="margin-bottom: 1rem;">
      <button @click="setFilter('all')">All</button>
      <button @click="setFilter('completed')">Completed</button>
      <button @click="setFilter('pending')">Pending</button>
      <router-link to="/create" style="margin-left: 1rem;">Create Task</router-link>
    </div>

    <div v-if="filteredTasks.length">
      <div v-for="task in filteredTasks" :key="task.id" style="margin-bottom: 0.5rem;">
        <router-link :to="`/task/${task.id}`">{{ task.title }}</router-link>
        <span> - {{ task.isCompleted ? '✅' : '🕒' }}</span>
        <button @click="toggle(task)">Toggle</button>
        <button @click="remove(task.id)">Delete</button>
      </div>
    </div>

    <div v-else>
      <p>No tasks found.</p>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { ref, computed, onMounted } from 'vue';
import api from '../api';

interface TaskItem {
  id: string;
  title: string;
  description: string;
  isCompleted: boolean;
  createdAt: string;
  completedAt: string | null;
}

const tasks = ref<TaskItem[]>([]);
const filter = ref<'all' | 'completed' | 'pending'>('all');

const fetchTasks = async () => {
  const res = await api.get('/');
  tasks.value = res.data;
};

const toggle = async (task: TaskItem) => {
  const updated = {
    ...task,
    isCompleted: !task.isCompleted,
    completedAt: !task.isCompleted ? new Date().toISOString() : null
  };
  await api.put(`/${task.id}`, updated);
  await fetchTasks();
};

const remove = async (id: string) => {
  await api.delete(`/${id}`);
  await fetchTasks();
};

const setFilter = (f: typeof filter.value) => {
  filter.value = f;
};

const filteredTasks = computed(() => {
  if (filter.value === 'completed') return tasks.value.filter(t => t.isCompleted);
  if (filter.value === 'pending') return tasks.value.filter(t => !t.isCompleted);
  return tasks.value;
});

onMounted(fetchTasks);
</script>
