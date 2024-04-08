import { type Component, type JSX, mergeProps } from "solid-js"

const GlassMorphism: Component<{
	class?: string
	id?: string
	children?: JSX.Element
}> = (props) => {
	const newProps = mergeProps({ class: "", id: "" }, props)
	return (
		<div
			id={newProps.id}
			class={`m-[1rem] p-[0.5rem] bg-gray-300 rounded-lg bg-clip-padding backdrop-filter backdrop-blur-lg bg-opacity-30 border ${newProps.class}`}
		>
			{newProps.children}
		</div>
	)
}

export default GlassMorphism
