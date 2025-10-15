<template>
  <div class="container">
    <h2>Wybod Task Manager</h2>

    <div class="toolbar">
      <button @click="setFilter('all')" :class="{ active: filter === 'all' }">All</button>
      <button @click="setFilter('completed')" :class="{ active: filter === 'completed' }">Completed</button>
      <button @click="setFilter('pending')" :class="{ active: filter === 'pending' }">Pending</button>
      <router-link to="/create" class="create-link">➕ Create Task</router-link>
    </div>

    <div v-if="filteredTasks.length" class="task-list">
      <div v-for="task in filteredTasks" :key="task.id" class="task-card">
        <div class="task-header">
          <router-link :to="`/task/${task.id}`" class="task-title">{{ task.title }}</router-link>
          <span class="status">{{ task.isCompleted ? '✅ Completed' : '🕒 Pending' }}</span>
        </div>
        <div class="task-actions">
          <button @click="toggle(task)">Toggle</button>
          <button @click="remove(task.id)">Delete</button>
        </div>
      </div>
    </div>

    <div v-else class="empty-state">
      <p>No tasks found. Try creating one!</p>
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

<style scoped>
.container {
  max-width: 600px;
  margin: 2rem auto;
  font-family: 'Segoe UI', sans-serif;
}

h2 {
  text-align: center;
  margin-bottom: 1rem;
}

.toolbar {
  display: flex;
  gap: 0.5rem;
  justify-content: center;
  margin-bottom: 1.5rem;
}

button {
  padding: 0.5rem 1rem;
  border: none;
  background-color: #eee;
  cursor: pointer;
  border-radius: 4px;
}

button.active {
  background-color: #0078d4;
  color: white;
}

.create-link {
  margin-left: auto;
  text-decoration: none;
  background-color: #28a745;
  color: white;
  padding: 0.5rem 1rem;
  border-radius: 4px;
}

.task-list {
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

.task-card {
  padding: 1rem;
  border: 1px solid #ccc;
  border-radius: 6px;
  background-color: #f9f9f9;
}

.task-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.task-title {
  font-weight: bold;
  text-decoration: none;
  color: #333;
}

.status {
  font-size: 0.9rem;
  color: #666;
}

.task-actions {
  margin-top: 0.5rem;
  display: flex;
  gap: 0.5rem;
}

.empty-state {
  text-align: center;
  color: #888;
}
</style>





<!--<template>-->
<!--  <div>-->
<!--    <h2>Tasks</h2>-->

<!--    <div style="margin-bottom: 1rem;">-->
<!--      <button @click="setFilter('all')">All</button>-->
<!--      <button @click="setFilter('completed')">Completed</button>-->
<!--      <button @click="setFilter('pending')">Pending</button>-->
<!--      <router-link to="/create" style="margin-left: 1rem;">Create Task</router-link>-->
<!--    </div>-->

<!--    <div v-if="filteredTasks.length">-->
<!--      <div v-for="task in filteredTasks" :key="task.id" style="margin-bottom: 0.5rem;">-->
<!--        <router-link :to="`/task/${task.id}`">{{ task.title }}</router-link>-->
<!--        <span> - {{ task.isCompleted ? '✅' : '🕒' }}</span>-->
<!--        <button @click="toggle(task)">Toggle</button>-->
<!--        <button @click="remove(task.id)">Delete</button>-->
<!--      </div>-->
<!--    </div>-->

<!--    <div v-else>-->
<!--      <p>No tasks found.</p>-->
<!--    </div>-->
<!--  </div>-->
<!--</template>-->

<!--<script lang="ts" setup>-->
<!--import { ref, computed, onMounted } from 'vue';-->
<!--import api from '../api';-->

<!--interface TaskItem {-->
<!--  id: string;-->
<!--  title: string;-->
<!--  description: string;-->
<!--  isCompleted: boolean;-->
<!--  createdAt: string;-->
<!--  completedAt: string | null;-->
<!--}-->

<!--const tasks = ref<TaskItem[]>([]);-->
<!--const filter = ref<'all' | 'completed' | 'pending'>('all');-->

<!--const fetchTasks = async () => {-->
<!--  const res = await api.get('/');-->
<!--  tasks.value = res.data;-->
<!--};-->

<!--const toggle = async (task: TaskItem) => {-->
<!--  const updated = {-->
<!--    ...task,-->
<!--    isCompleted: !task.isCompleted,-->
<!--    completedAt: !task.isCompleted ? new Date().toISOString() : null-->
<!--  };-->
<!--  await api.put(`/${task.id}`, updated);-->
<!--  await fetchTasks();-->
<!--};-->

<!--const remove = async (id: string) => {-->
<!--  await api.delete(`/${id}`);-->
<!--  await fetchTasks();-->
<!--};-->

<!--const setFilter = (f: typeof filter.value) => {-->
<!--  filter.value = f;-->
<!--};-->

<!--const filteredTasks = computed(() => {-->
<!--  if (filter.value === 'completed') return tasks.value.filter(t => t.isCompleted);-->
<!--  if (filter.value === 'pending') return tasks.value.filter(t => !t.isCompleted);-->
<!--  return tasks.value;-->
<!--});-->

<!--onMounted(fetchTasks);-->
<!--</script>-->
