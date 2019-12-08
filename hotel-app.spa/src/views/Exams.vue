<template>
  <div>
    <div v-highlight>
      <pre class="language-javascript"><code>{{ inputCode }}</code></pre>
    </div>
  </div>
</template>

<script>

export default { 
  data() {
    return { 
      inputCode: `var numbers = [ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 ]; 
numbers.sort((a, b) => { 
  if(a > b) return 1; 
  else return -1; 
});`
    }
  }
};

</script>

<style>
  .v-application code {
    -webkit-box-shadow: none !important;
    box-shadow: none !important;
  }
</style>